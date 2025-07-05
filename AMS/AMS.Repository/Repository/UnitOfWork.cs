using AMS.Repository.Models;
using System;

namespace AMS.Repository.Repository
{
    public class UnitOfWork : IDisposable
    {
        private TestContext context = new TestContext();

        #region Model Variables
        private GenericRepository<Appointment> _appointment;
      
        #endregion

        #region Interface Implementation
        public GenericRepository<Appointment> Appointment
        {
            get
            {
                if (_appointment == null)
                    _appointment = new GenericRepository<Appointment>(context);
                return _appointment;
            }
        }
        
        #endregion

        #region Save and Dispose
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
