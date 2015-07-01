﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Q3Client
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public static readonly DependencyProperty HubProperty =
            DependencyProperty.Register("Hub", typeof (Hub), typeof (Header));


        public Header()
        {
            InitializeComponent();
        }

        public async void NewQueueClicked(object sender, RoutedEventArgs e)
        {
            var window = new NewQueue(GroupsCache);
            window.Owner = ParentQueueList;
            window.ShowDialog();

            if (!string.IsNullOrWhiteSpace(window.NewQueueName))
            {
                var hub = Hub;
                Dispatcher.InvokeAsync(() => hub.CreateQueue(window.NewQueueName, (string)window.GroupSelector.SelectedValue));
            }
            Win32.IsApplicationActive();
        }

        public Hub Hub {
            get { return (Hub) GetValue(HubProperty); }
            set { SetValue(HubProperty, value);}
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ParentQueueList.WindowStateExtended = QueueList.eWindowStateExtended.Closed;
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ParentQueueList.WindowStateExtended = QueueList.eWindowStateExtended.Minimized;
        }

        private QueueList ParentQueueList
        {
            get { return (QueueList) Window.GetWindow(this); }
        }

        public GroupsCache GroupsCache { get; set; }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).DragMove();
            ParentQueueList.AdjustHeight();
        }
    }
}
