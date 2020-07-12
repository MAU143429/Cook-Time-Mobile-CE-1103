using System;
using System.Collections.Generic;
using System.Text;

namespace CookTime.User
{
    public interface iNotification
    {
        void CreateNotification(String title, String message);
    }
}
