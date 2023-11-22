using Microsoft.Extensions.DependencyInjection;
using BookApp;
using BookApp.Services;
using BookApp.Repositories;
using BookApp.Entities;
using BookApp.DataProviders;
using BookApp.Components.CsvReader;
using BookApp.Components.XmlReader;
using Microsoft.EntityFrameworkCore;
using BookApp.Data;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Book>, SqlRepository<Book>>();
services.AddSingleton<IRepository<BookOwner>, SqlRepository<BookOwner>>();

services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandler, BookApp.Services.EventHandler>();
services.AddSingleton<IDataProvider, DataProvider>();

services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlReader, XmlReader>();

services.AddDbContext<BookAppDbContext>(options => options
.UseSqlServer("Data Source=DESKTOP-VC0LUU3\\SQLEXPRESS;Initial Catalog=\"BookAppStorage\";Integrated Security=True;Encrypt=False"));

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();
app.Run();