using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsersMS.Commons.Enums;
using UsersMS.Domain.Entities;
using UsersMS.Infrastructure.DataBase;
using UsersMS.Infrastructure.Exceptions;
using UsersMS.Infrastructure.Repositories;

[TestClass]
public class ProveedorRepositoryTests
{
    private UsersDbContext _dbContext;
    private ProveedorRepository _repositorio;

    public ProveedorRepositoryTests()
    { // Inicializar con un valor por defecto
        _dbContext = null!;
        _repositorio = null!;
    }

    [TestInitialize]
    public void Initialize()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "TestProveedorDatabase")
            .Options;

        _dbContext = new UsersDbContext(options);
        _repositorio = new ProveedorRepository(_dbContext);
    }

    [TestMethod]
    public async Task AddAsync_DeberiaAgregarProveedor()
    {
        // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos"
        ,
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        // Act
        await _repositorio.AddAsync(Proveedor);

        // Assert
        var ProveedorAgregado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.IsNotNull(ProveedorAgregado);
    }

    [TestMethod]
    [ExpectedException(typeof(ProveedorNotFoundException))]
    public async Task DeleteAsync_DeberiaLanzarExcepcionSiProveedorNoEsEncontrado()
    { // Arrange
        var ProveedorId = Guid.NewGuid();
        // Act
        await _repositorio.DeleteAsync(ProveedorId);
    }

    [TestMethod]
    public async Task DeleteAsync_DeberiaEliminarProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };
        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();

        // Act
        await _repositorio.DeleteAsync(Proveedor.ProveedorId);

        // Assert
        var ProveedorEliminado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.IsNull(ProveedorEliminado);
    }


    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarNombreProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.Name = "Nombre Actualizado";

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual("Nombre Actualizado", ProveedorActualizado?.Name);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarApellidoProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.Apellido = "Apellido Actualizado";

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual("Apellido Actualizado", ProveedorActualizado?.Apellido);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarEmailProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.Email = "Email Actualizado";

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual("Email Actualizado", ProveedorActualizado?.Email);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarCedulaProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.Cedula = "Cedula Actualizada";

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual("Cedula Actualizada", ProveedorActualizado?.Cedula);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarDepartamentoIdProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.DepartamentoId = Guid.NewGuid();

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual(Proveedor.DepartamentoId, ProveedorActualizado?.DepartamentoId);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarEmpresaIdProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.EmpresaId = Guid.NewGuid();

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual(Proveedor.EmpresaId, ProveedorActualizado?.EmpresaId);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarPasswordProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.Password = "Password Actualizada";

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual("Password Actualizada", ProveedorActualizado?.Password);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarStateProveedor()
    { // Arrange
        var Proveedor = new Proveedor
        {
            ProveedorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Proveedores.AddAsync(Proveedor);
        await _dbContext.SaveChangesAsync();
        Proveedor.State = UserState.Inactive;

        // Act
        await _repositorio.UpdateAsync(Proveedor);

        // Assert

        var ProveedorActualizado = await _dbContext.Proveedores.FindAsync(Proveedor.ProveedorId);
        Assert.AreEqual(UserState.Inactive, ProveedorActualizado?.State);
    }

    [TestMethod]
    public async Task GetByIdAsync_DeberiaRetornarProveedorPorId()
    { // Arrange
        var Id = Guid.NewGuid();
        var Proveedor = new Proveedor
        {
            ProveedorId = Id,
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };
        await _dbContext.Proveedores.AddAsync(Proveedor); await _dbContext.SaveChangesAsync();
        // Act
        var resultado = await _repositorio.GetByIdAsync(Id);
        // Assert
        Assert.AreEqual(Proveedor, resultado);
    }

    [TestMethod]
    public async Task GetAllAsync_DeberiaRetornarTodosLosProveedores()
    { // Arrange
        var Proveedores = new List<Proveedor> { new Proveedor
            {
                ProveedorId = Guid.NewGuid(),
                Email = "leo@gmail.com",
                Apellido = "Santos",
                Cedula = "27904398",
                DepartamentoId = Guid.NewGuid(),
                EmpresaId = Guid.NewGuid(),
                Name = "Leoanrdo",
                Password = "1234",
                Rol = 0,
                State = 0
            },
            new Proveedor
            {
                ProveedorId = Guid.NewGuid(),
                Email = "richard@gmail.com",
                Apellido = "morales",
                Cedula = "2554489",
                DepartamentoId = Guid.NewGuid(),
                EmpresaId = Guid.NewGuid(),
                Name = "Richardison",
                Password = "7841a",
                Rol = 0,
                State = UserState.Inactive
            }
      };

        var todosLosProveedores = await _dbContext.Proveedores.ToListAsync();
        _dbContext.Proveedores.RemoveRange(todosLosProveedores);

        await _dbContext.Proveedores.AddRangeAsync(Proveedores);
        await _dbContext.SaveChangesAsync();

        //Act
        var resultado = await _repositorio.GetAllAsync();

        // Assert
        Assert.AreEqual(Proveedores.Count, resultado?.Count);
    }
}



