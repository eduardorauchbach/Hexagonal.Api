﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Session.Implementation
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IDbSession _session;

        public UnitOfWork(IDbSession session)
        {
            _session = session;
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_session.Transaction != null)
            {
                _session.Transaction.Commit();
            }
            Dispose();
        }

        public void Rollback()
        {
            if (_session.Transaction != null)
            {
                _session.Transaction.Rollback();
            }
            Dispose();
        }

        public void Dispose()
        {
            _session.Transaction?.Dispose();
            _session.Transaction = null;
        }
    }
}
