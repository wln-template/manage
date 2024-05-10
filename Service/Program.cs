using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Wlniao;
using Wlniao.Swagger;
using Wlniao.XServer;
var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.WebHost.UseKestrel(o => { o.ListenAnyIP(XCore.ListenPort); o.AllowSynchronousIO = true; });
builder.Services.AddRouting(o => { o.LowercaseUrls = true; });
builder.Services.AddControllers();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSwaggerExtend(new System.Collections.Generic.List<ApiGroupInfo> {
    new ApiGroupInfo { Name = "Service", Title = "对外开放服务接口" },
    new ApiGroupInfo { Name = "Manager", Title = "内部管理服务接口" }
});
Wlniao.Log.Loger.Console("Now listening on: http://127.0.0.1:" + XCore.ListenPort, ConsoleColor.DarkGreen);
XCore.CloseServerCertificateValidation();
DbConnectInfo.WLN_CONNSTR_MYSQL.IsNullOrEmpty();
var app = builder.Build();
if (true || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseMiddleware<Wlniao.Middleware.ErrorHandling>();
    app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()).UseStaticFiles();
    if (XStorage.Local.Using)
    {
        var folder = Wlniao.IO.PathTool.Map(XStorage.Local.Path);
        if (!System.IO.Directory.Exists(folder))
        {
            System.IO.Directory.CreateDirectory(folder);
        }
        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(folder),
            RequestPath = new PathString()
        });
    }
    app.UseKnife4UI(o => { o.RoutePrefix = "swagger"; ApiGroupInfo.GroupInfos.ForEach(group => { o.SwaggerEndpoint(group.ApiUrl, group.Title); }); });
    SqlContext.Init();
}
app.MapControllers();
app.Run();