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
public class ConductorRepositoryTests
{
    private UsersDbContext _dbContext;
    private ConductorRepository _repositorio;

    public ConductorRepositoryTests()
    { // Inicializar con un valor por defecto
        _dbContext = null!;
        _repositorio = null!;
    }
    [TestInitialize]
    public void Initialize()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "TestCondcutorDatabase")
            .Options;

        _dbContext = new UsersDbContext(options);
        _repositorio = new ConductorRepository(_dbContext);
    }

    [TestMethod]
    public async Task AddAsync_DeberiaAgregarConductor()
    {
        // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        // Act
        await _repositorio.AddAsync(Conductor);

        // Assert
        var ConductorAgregado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.IsNotNull(ConductorAgregado);
    }

    [TestMethod]
    [ExpectedException(typeof(ConductorNotFoundException))]
    public async Task DeleteAsync_DeberiaLanzarExcepcionSiConductorNoEsEncontrado()
    { // Arrange
        var ConductorId = Guid.NewGuid();
        // Act
        await _repositorio.DeleteAsync(ConductorId);
    }

    [TestMethod]
    public async Task DeleteAsync_DeberiaEliminarConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };
        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();

        // Act
        await _repositorio.DeleteAsync(Conductor.ConductorId);

        // Assert
        var ConductorEliminado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.IsNull(ConductorEliminado);
    }


    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarNombreConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.Name = "Nombre Actualizado";

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual("Nombre Actualizado", ConductorActualizado?.Name);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarApellidoConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.Apellido = "Apellido Actualizado";

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual("Apellido Actualizado", ConductorActualizado?.Apellido);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarEmailConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.Email = "Email Actualizado";

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual("Email Actualizado", ConductorActualizado?.Email);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarCedulaConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.Cedula = "Cedula Actualizada";

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual("Cedula Actualizada", ConductorActualizado?.Cedula);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarDepartamentoIdConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.DepartamentoId = Guid.NewGuid();

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual(Conductor.DepartamentoId, ConductorActualizado?.DepartamentoId);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarEmpresaIdConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.EmpresaId = Guid.NewGuid();

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual(Conductor.EmpresaId, ConductorActualizado?.EmpresaId);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarPasswordConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.Password = "Password Actualizada";

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual("Password Actualizada", ConductorActualizado?.Password);
    }

    [TestMethod]
    public async Task UpdateAsync_DeberiaActualizarStateConductor()
    { // Arrange
        var Conductor = new Conductor
        {
            ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };

        await _dbContext.Conductores.AddAsync(Conductor);
        await _dbContext.SaveChangesAsync();
        Conductor.State = UserState.Inactive;

        // Act
        await _repositorio.UpdateAsync(Conductor);

        // Assert

        var ConductorActualizado = await _dbContext.Conductores.FindAsync(Conductor.ConductorId);
        Assert.AreEqual(UserState.Inactive, ConductorActualizado?.State);
    }

    [TestMethod]
    public async Task GetByIdAsync_DeberiaRetornarConductorPorId()
    { // Arrange
        var Id = Guid.NewGuid();
        var Conductor = new Conductor
        {
            ConductorId = Id,
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
        };
        await _dbContext.Conductores.AddAsync(Conductor); await _dbContext.SaveChangesAsync();
        // Act
        var resultado = await _repositorio.GetByIdAsync(Id);
        // Assert
        Assert.AreEqual(Conductor, resultado);
    }

    [TestMethod]
    public async Task GetAllAsync_DeberiaRetornarTodosLosConductores()
    { // Arrange
        var Conductores = new List<Conductor> { new Conductor
            {
                ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
            },
            new Conductor
            {
                ConductorId = Guid.NewGuid(),
            Email = "leo@gmail.com",
            Apellido = "Santos",
            Cedula = "27904398",
            Licencia = true,
            CertificadoSalud = true,
            DepartamentoId = Guid.NewGuid(),
            EmpresaId = Guid.NewGuid(),
            Name = "Leoanrdo",
            Password = "1234",
            Rol = 0,
            State = 0
            }
      };

        var todosLosConductores = await _dbContext.Conductores.ToListAsync();
        _dbContext.Conductores.RemoveRange(todosLosConductores);

        await _dbContext.Conductores.AddRangeAsync(Conductores);
        await _dbContext.SaveChangesAsync();

        //Act
        var resultado = await _repositorio.GetAllAsync();

        // Assert
        Assert.AreEqual(Conductores.Count, resultado?.Count);
    }
}




