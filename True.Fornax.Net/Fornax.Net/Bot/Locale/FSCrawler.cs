﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Fornax.Net.Bot.Locale
{
    partial class FSCrawler : ServiceBase
    {
        public FSCrawler() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop() {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
