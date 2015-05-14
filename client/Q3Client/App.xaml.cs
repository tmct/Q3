﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Q3Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Hub hub;
        private User user;
        private QueueUpdater queueUpdater;



        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            user = GetUser();

            hub = new Hub(user);
            queueUpdater = new QueueUpdater(hub, user);


            hub.QueueMembershipChanged += QueueMembershipChanged;
            hub.QueueCreated += QueueCreated;
            hub.QueueStatusChanged += QueueStatusChanged;

            await queueUpdater.RefreshALl();

        }
        private void QueueStatusChanged(object sender, QueueActionEventArgs args)
        {
            var queue = args.Queue;
            Dispatcher.Invoke(() => queueUpdater.UpdateQueue(queue));
        }

        private void QueueMembershipChanged(object sender, QueueActionEventArgs args)
        {
            Dispatcher.Invoke(() => queueUpdater.UpdateQueue(args.Queue));
        }


        private void QueueCreated(object sender, QueueActionEventArgs args)
        {
            var queue = args.Queue;

            Dispatcher.Invoke(() => queueUpdater.AddQueue(queue));
        }

        private User GetUser()
        {
            var principal = UserPrincipal.Current;

            var user = new User()
            {
                UserName = principal.SamAccountName,
                EmailAddress = principal.EmailAddress,
                FullName = principal.DisplayName
            };


#if DEBUG
            var suffix = (DateTime.Now.Ticks % 100).ToString();
            user.UserName = user.UserName + suffix;
            user.FullName = user.FullName + suffix;
#endif

            return user;
        }

    }
}
