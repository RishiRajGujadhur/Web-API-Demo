﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAPIDemo.Startup))]
namespace WebAPIDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
