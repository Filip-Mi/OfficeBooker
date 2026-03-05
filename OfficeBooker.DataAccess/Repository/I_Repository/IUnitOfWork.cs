namespace OfficeBooker.DataAccess.Repository.I_Repository
{
    public interface IUnitOfWork 
    {
        public IReservationRepository reservationRepository { get; }
        public IOfficeRepository officeRepository { get; }
        void Save();
    }
}
