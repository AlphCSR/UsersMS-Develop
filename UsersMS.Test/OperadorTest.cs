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
public class OperadorRepositoryTests
{
    private UsersDbContext _dbContext;
    private OperadorRepository _repositorio;

    public OperadorRepositoryTests()
    { // Inicializar con un valor por defecto
        _dbContext = null!;
        _repositorio = null!;
    }

    [TestInitialize]
    public void Initialize()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "TestOperadorDatabase")
            .Options;

        _dbContext = new UsersDbContext(options);
        _repositorio = new OperadorRepository(_dbContext);
    }

    [TestMethod]
    public async Task AddAsync_DeberiaAgregarOperador()
    {
        // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos"
        ,
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        // Act
        await _repositorio.AddAsync(Operador);

        // Assert
        var OperadorAgregado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.IsNotNull(OperadorAgregado);
    }

    [TestMethod]
    [ExpectedException(typeof(OperadorNotFoundException))]
    public async Task DeleteAsync_DeberiaLanzarExcepcionSiOperadorNoEsEncontrado()
    { // Arrange
        var OperadorId = Guid.NewGuid();
        // Act
        await _repositorio.DeleteAsync(OperadorId);
    }

    [TestMethod]
    public async Task DeleteAsync_DeberiaEliminarOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
           
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };
        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();

        // Act
        await _repositorio.DeleteAsync(Operador.OperadorId);

        // Assert
        var OperadorEliminado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.IsNull(OperadorEliminado);
    }


    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarNombreOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();
        Operador.Name = "Nombre Actualizado";

        // Act
        await _repositorio.UpdateAsync(Operador);

        // Assert

        var OperadorActualizado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.AreEqual("Nombre Actualizado", OperadorActualizado?.Name);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarApellidoOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
           
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();
        Operador.Apellido = "Apellido Actualizado";

        // Act
        await _repositorio.UpdateAsync(Operador);

        // Assert

        var OperadorActualizado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.AreEqual("Apellido Actualizado", OperadorActualizado?.Apellido);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarEmailOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();
        Operador.Email = "Email Actualizado";

        // Act
        await _repositorio.UpdateAsync(Operador);

        // Assert

        var OperadorActualizado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.AreEqual("Email Actualizado", OperadorActualizado?.Email);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarCedulaOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();
        Operador.Cedula = "Cedula Actualizada";

        // Act
        await _repositorio.UpdateAsync(Operador);

        // Assert

        var OperadorActualizado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.AreEqual("Cedula Actualizada", OperadorActualizado?.Cedula);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarDepartamentoIdOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
            
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();
        Operador.DepartamentoId = Guid.NewGuid();

        // Act
        await _repositorio.UpdateAsync(Operador);

        // Assert

        var OperadorActualizado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.AreEqual(Operador.DepartamentoId, OperadorActualizado?.DepartamentoId);
    }


    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarPasswordOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
        
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();
        Operador.Password = "Password Actualizada";

        // Act
        await _repositorio.UpdateAsync(Operador);

        // Assert

        var OperadorActualizado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.AreEqual("Password Actualizada", OperadorActualizado?.Password);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarStateOperador()
    { // Arrange
        var Operador = new Operador
        {
            OperadorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
          
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Operadores.AddAsync(Operador);
        await _dbContext.SaveChangesAsync();
        Operador.State = UserState.Inactive;

        // Act
        await _repositorio.UpdateAsync(Operador);

        // Assert

        var OperadorActualizado = await _dbContext.Operadores.FindAsync(Operador.OperadorId);
        Assert.AreEqual(UserState.Inactive, OperadorActualizado?.State);
    }

    [TestMethod]
    public async Task GetByIdAsync_DeberiaRetornarOperadorPorId()
    { // Arrange
        var Id = Guid.NewGuid();
        var Operador = new Operador
        {
            OperadorId = Id,
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            DepartamentoId = Guid.NewGuid(),
         
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };
        await _dbContext.Operadores.AddAsync(Operador); await _dbContext.SaveChangesAsync();
        // Act
        var resultado = await _repositorio.GetByIdAsync(Id);
        // Assert
        Assert.AreEqual(Operador, resultado);
    }

    [TestMethod]
    public async Task GetAllAsync_DeberiaRetornarTodosLosOperadores()
    { // Arrange
        var Operadores = new List<Operador> { new Operador
            {
                OperadorId = Guid.NewGuid(),
                Email = "leo@gmail.com",
                Apellido = "Santos",
                Cedula = "27904398",
                DepartamentoId = Guid.NewGuid(),
          
                Name = "Leoanrdo",
                Password = "1234",
                Rol = 0,
                State = 0
            },
            new Operador
            {
                OperadorId = Guid.NewGuid(),
                Email = "richard@gmail.com",
                Apellido = "morales",
                Cedula = "2554489",
                DepartamentoId = Guid.NewGuid(),
               
                Name = "Richardison",
                Password = "7841a",
                Rol = 0,
                State = UserState.Inactive
            }
      };

        var todosLosOperadores = await _dbContext.Operadores.ToListAsync();
        _dbContext.Operadores.RemoveRange(todosLosOperadores);

        await _dbContext.Operadores.AddRangeAsync(Operadores);
        await _dbContext.SaveChangesAsync();

        //Act
        var resultado = await _repositorio.GetAllAsync();

        // Assert
        Assert.AreEqual(Operadores.Count, resultado?.Count);
    }
}




