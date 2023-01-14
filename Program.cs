using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Automapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IRepositorioCadetes, RepositorioCadetes>();
builder.Services.AddTransient<IRepositorioClientes, RepositorioClientes>();
builder.Services.AddTransient<IRepositorioPedidos, RepositorioPedidos>();
builder.Services.AddTransient<IRepositorioCadeterias, RepositorioCadeterias>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opciones => {
    opciones.IdleTimeout = TimeSpan.FromMinutes(15);
    opciones.Cookie.HttpOnly = true;
    opciones.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
