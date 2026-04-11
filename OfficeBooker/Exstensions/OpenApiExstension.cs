using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using System.Xml;

namespace OfficeBooker.API.Extensions
{
    public static class OpenApiExtensions
    {
        /// <summary>
        /// Injects XML documentation from all project files into OpenAPI/Scalar.
        /// </summary>
        public static void AddXmlDocumentation(this OpenApiOptions options)
        {
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            var xmlDocs = new List<XmlDocument>();

            foreach (var file in xmlFiles)
            {
                try
                {
                    var doc = new XmlDocument();
                    doc.Load(file);
                    xmlDocs.Add(doc);
                }
                catch { }
            }

            if (xmlDocs.Count == 0) return;

            // Mapping for Controller Methods
            options.AddOperationTransformer((operation, context, cancellationToken) =>
            {
                if (context.Description.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                {
                    var methodInfo = controllerActionDescriptor.MethodInfo;
                    var typeName = methodInfo.DeclaringType?.FullName;
                    var memberName = $"M:{typeName}.{methodInfo.Name}";

                    foreach (var xml in xmlDocs)
                    {
                        var node = xml.SelectSingleNode($"/doc/members/member[starts-with(@name, '{memberName}')]/summary");
                        if (node != null)
                        {
                            operation.Description = node.InnerText.Trim();
                            operation.Summary = null;
                            break;
                        }
                    }
                }
                return Task.CompletedTask;
            });

            // Mapping for DTO Properties
            options.AddSchemaTransformer((schema, context, cancellationToken) =>
            {
                var type = context.JsonTypeInfo?.Type;
                if (type == null || schema.Properties == null) return Task.CompletedTask;

                foreach (var property in schema.Properties)
                {
                    var propInfo = type.GetProperty(property.Key,
                        BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.FlattenHierarchy);

                    if (propInfo != null)
                    {
                        var memberName = $"P:{type.FullName}.{propInfo.Name}";

                        foreach (var xml in xmlDocs)
                        {
                            var node = xml.SelectSingleNode($"/doc/members/member[@name='{memberName}']/summary");
                            if (node != null)
                            {
                                property.Value.Description = node.InnerText.Trim();
                                break;
                            }
                        }
                    }
                }
                return Task.CompletedTask;
            });
        }
    }
}