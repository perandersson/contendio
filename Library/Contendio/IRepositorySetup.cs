using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contendio
{
    public interface IRepositorySetup
    {
        /// <summary>
        /// Installs the content repository
        /// </summary>
        void Install();

        /// <summary>
        /// Checks if the content repository is installed
        /// </summary>
        /// <returns></returns>
        bool IsInstalled();

        /// <summary>
        /// Uninstalls the entire content repository
        /// </summary>
        void Uninstall();
    }
}
