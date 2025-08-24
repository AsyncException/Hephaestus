using Hephaestus.Sample.Module.AuditLog.AspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hephaestus.Sample.Module.AuditLog.AspNet;

//This would normally be a database or file storage.
public class ConfigProvider
{
    public List<AuditLogConfiguration> Config { get; set; } = [];
}
