﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Infrastructure.Context
{
    public class Disposable : IDisposable
    {
        private bool isDisposed;

        ~Disposable()
            => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
                DisposeCore();

            isDisposed = true;
        }

        protected virtual void DisposeCore() { }
    }
}
