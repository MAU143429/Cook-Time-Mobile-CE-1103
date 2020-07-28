using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime.User
{
    /// <summary>
    /// Interface for creating new notifications 
    /// author Jose Antonio Espinoza.
    /// </summary>
    public interface iNotification
    {
        void CreateNotification(String title, String message);
    }
}
