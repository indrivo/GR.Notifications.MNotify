# GR.Notifications.MNotify
Implementation of gov service MNotify

[![Build Status](https://travis-ci.com/indrivo/GR.Notifications.MNotify.svg?branch=main)](https://travis-ci.com/indrivo/GR.Notifications.MNotify)

[![.NET](https://github.com/indrivo/GR.Notifications.MNotify/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/indrivo/GR.Notifications.MNotify/actions/workflows/dotnet.yml)

[![NuGet](https://img.shields.io/nuget/v/GR.Notifications.MNotify.svg)](https://www.nuget.org/packages/GR.Notifications.MNotify)
[![Nuget](https://img.shields.io/nuget/dt/GR.Notifications.MNotify)](https://www.nuget.org/packages/GR.Notifications.MNotify)


# Usage

1. Install package from [nuget](https://www.nuget.org/packages/GR.Notifications.MNotify/)

```powershell
Install-Package GR.Notifications.MNotify
```

2. Register the services:

```C#
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    //...

    public void ConfigureServices(IServiceCollection services)
    {
        //...
        services.AddMNotify(Configuration);
        //...
    }

    //...
}
```

3. Add configuration on your appsettings.json file:

```json
{
  "MNotifyOptions": {
    "ServiceClientAddress": "https://testmnotify.gov.md:8443/MNotify.svc",
    "ServiceCertificatePath": "<certificate file name>.pfx",
    "ServiceCertificatePassword": "<certificate password>"
  }
}
```

`ServiceClientAddress` - url of service

`ServiceCertificatePath` - physical path to pfx certificate

`ServiceCertificatePassword` - certificate password

4. Inject MNotify Service and send notifications

```C#
public class FooService{
    private readonly IMNotifyService _service;

    public FooService(IMNotifyService _service){
        _service = service;
    }

    public async Task FooMethodAsync(){
        var sendResult = await _service.SentNotificationAsync(new MNotifyNotification
        {
            Sender = new MNotifyPerson("UserName", "foo@foo.com"),
            Recipient = new MNotifyPerson("UserName", "foo@foo.com"),
            NotificationType = "Test",
            Subject = "Notification subject",
            Content = "Test notification"
        });
    }
}
```