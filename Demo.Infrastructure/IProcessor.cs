﻿using System;

namespace Demo.Infrastructure.Processors
{
    public interface IProcessor:IDisposable
    {
        string Name { get; }
        /// <summary>
        /// Loop interval seconds
        /// </summary>
        int LoopInterval { get; set; }
        void DoProcess(object obj=null);
    }
}
