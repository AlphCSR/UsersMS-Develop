using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersMS.Infrastructure.DataBase;
using UsersMS.Infrastructure.Repositories;
using UsersMS.Domain.Entities;
using UsersMS.Infrastructure.Exceptions;
using UsersMS.Commons.Enums;

[TestClass]
public class AdministradorRepositoryTests
{
    private UsersDbContext _dbContext;
    private AdministradorRepository _repositorio;

    public AdministradorRepositoryTests()
    { // Inicializar con un valor por defecto
      _dbContext = null!; 
      _repositorio = null!; 
    }

    [TestInitialize]
    public void Initialize()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new UsersDbContext(options);
        _repositorio = new AdministradorRepository(_dbContext);
    }

    [TestMethod]
    public async Task AddAsync_DeberiaAgregarAdministrador()
    {
        // Arrange
        var administrador = new Administrador { AdministradorId = Guid.NewGuid(), Email = "leo@gmail.com", Apellido="Santos"
        , Cedula = "27904398", DepartamentoId = Guid.NewGuid(), EmpresaId = Guid.NewGuid(), Name ="Leoanrdo", Password = "1234",
         Rol = 0, State = 0};

        // Act
        await _repositorio.AddAsync(administrador);

        // Assert
        var administradorAgregado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.IsNotNull(administradorAgregado);
    }

    [TestMethod]
    [ExpectedException(typeof(AdministradorNotFoundException))]
    public async Task DeleteAsync_DeberiaLanzarExcepcionSiAdministradorNoEsEncontrado()
    { // Arrange
      var administradorId = Guid.NewGuid(); 
      // Act
      await _repositorio.DeleteAsync(administradorId); 
    }

    [TestMethod]
    public async Task DeleteAsync_DeberiaEliminarAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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
        await _dbContext.Administradores.AddAsync(administrador); 
        await _dbContext.SaveChangesAsync(); 
        
     // Act
     await _repositorio.DeleteAsync(administrador.AdministradorId); 
        
     // Assert
     var administradorEliminado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId); 
     Assert.IsNull(administradorEliminado); 
    }


    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarNombreAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador); 
        await _dbContext.SaveChangesAsync(); 
        administrador.Name = "Nombre Actualizado"; 
     
     // Act
     await _repositorio.UpdateAsync(administrador); 
        
     // Assert
     
        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId); 
        Assert.AreEqual("Nombre Actualizado", administradorActualizado?.Name); 
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarApellidoAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador);
        await _dbContext.SaveChangesAsync();
        administrador.Apellido = "Apellido Actualizado";

        // Act
        await _repositorio.UpdateAsync(administrador);

        // Assert

        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.AreEqual("Apellido Actualizado", administradorActualizado?.Apellido);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarEmailAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador);
        await _dbContext.SaveChangesAsync();
        administrador.Email = "Email Actualizado";

        // Act
        await _repositorio.UpdateAsync(administrador);

        // Assert

        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.AreEqual("Email Actualizado", administradorActualizado?.Email);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarCedulaAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador);
        await _dbContext.SaveChangesAsync();
        administrador.Cedula = "Cedula Actualizada";

        // Act
        await _repositorio.UpdateAsync(administrador);

        // Assert

        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.AreEqual("Cedula Actualizada", administradorActualizado?.Cedula);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarDepartamentoIdAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador);
        await _dbContext.SaveChangesAsync();
        administrador.DepartamentoId = Guid.NewGuid();

        // Act
        await _repositorio.UpdateAsync(administrador);

        // Assert

        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.AreEqual(administrador.DepartamentoId, administradorActualizado?.DepartamentoId);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarEmpresaIdAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador);
        await _dbContext.SaveChangesAsync();
        administrador.EmpresaId = Guid.NewGuid();

        // Act
        await _repositorio.UpdateAsync(administrador);

        // Assert

        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.AreEqual(administrador.EmpresaId, administradorActualizado?.EmpresaId);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarPasswordAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador);
        await _dbContext.SaveChangesAsync();
        administrador.Password = "Password Actualizada";

        // Act
        await _repositorio.UpdateAsync(administrador);

        // Assert

        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.AreEqual("Password Actualizada", administradorActualizado?.Password);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarStateAdministrador()
    { // Arrange
        var administrador = new Administrador
        {
            AdministradorId = Guid.NewGuid(),
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

        await _dbContext.Administradores.AddAsync(administrador);
        await _dbContext.SaveChangesAsync();
        administrador.State = UserState.Inactive;

        // Act
        await _repositorio.UpdateAsync(administrador);

        // Assert

        var administradorActualizado = await _dbContext.Administradores.FindAsync(administrador.AdministradorId);
        Assert.AreEqual(UserState.Inactive, administradorActualizado?.State);
    }

    [TestMethod]
    public async Task GetByIdAsync_DeberiaRetornarAdministradorPorId()
    { // Arrange
      var Id = Guid.NewGuid();
        var administrador = new Administrador
        {
            AdministradorId = Id,
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
        await _dbContext.Administradores.AddAsync(administrador); await _dbContext.SaveChangesAsync(); 
        // Act
        var resultado = await _repositorio.GetByIdAsync(Id); 
        // Assert
        Assert.AreEqual(administrador, resultado); 
    }

    [TestMethod]
    public async Task GetAllAsync_DeberiaRetornarTodosLosAdministradores()
    { // Arrange
      var administradores = new List<Administrador> { new Administrador
            {
                AdministradorId = Guid.NewGuid(),
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
            new Administrador 
            {
                AdministradorId = Guid.NewGuid(),
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

        var todosLosAdministradores = await _dbContext.Administradores.ToListAsync(); 
        _dbContext.Administradores.RemoveRange(todosLosAdministradores);

        await _dbContext.Administradores.AddRangeAsync(administradores);
        await _dbContext.SaveChangesAsync(); 
        
     //Act
     var resultado = await _repositorio.GetAllAsync(); 
        
     // Assert
     Assert.AreEqual(administradores.Count, resultado?.Count); 
    }
}



