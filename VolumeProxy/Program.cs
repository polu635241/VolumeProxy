using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VolumeProxy
{
    class Program
    {
        static void Main (string[] args)
        {
            VolumeController volumeController = new VolumeController ();
            volumeController.Init ();
            SpinWait.SpinUntil (() => false);
        }
    }
}
