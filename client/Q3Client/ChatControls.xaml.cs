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
using System.Windows.Threading;

namespace Q3Client
{
    /// <summary>
    /// Interaction logic for ChatControls.xaml
    /// </summary>
    public partial class ChatControls : UserControl
    {
        public class MessageEventArgs : EventArgs
        {
            public string Message { get; private set; }

            public MessageEventArgs(string message)
            {
                Message = message;
            }
        }

        public ChatControls(User user)
        {
            InitializeComponent();

            AvatarCanvas.Children.Add(new Avatar(user));

            MessageText.KeyUp += MessageTextOnKeyUp;

            this.IsVisibleChanged += ChatControls_IsVisibleChanged;
        }

        private void ChatControls_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                this.Dispatcher.BeginInvoke((Action)delegate
                {
                    Keyboard.Focus(MessageText);
                }, DispatcherPriority.Render);
            }
        }

        private void MessageTextOnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Enter && !string.IsNullOrWhiteSpace(MessageText.Text))
            {
                MessageSubmitted.SafeInvoke(this, new MessageEventArgs(MessageText.Text));
                ClearAndHide();
            }
            if (keyEventArgs.Key == Key.Escape)
            {
                ClearAndHide();
            }
        }

        private void ClearAndHide()
        {
            MessageText.Clear();
            this.Visibility = Visibility.Collapsed;
        }
        public event EventHandler<MessageEventArgs> MessageSubmitted;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearAndHide();
        }
    }
}
