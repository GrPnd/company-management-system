using App.BLL.Services;
using App.DAL.Contracts.Repositories;
using App.DAL.Contracts;
using Base.BLL.Contracts;
using Moq;
using Task = System.Threading.Tasks.Task;
using UserInTeamInTask = App.BLL.DTO.UserInTeamInTask;

namespace App.Tests.Unit;

public class UserInTeamInTaskServiceTest
{
    private readonly Mock<IAppUOW> _uowMock;
    private readonly Mock<IUserInTeamInTaskRepository> _repoMock;
    private readonly Mock<IBLLMapper<UserInTeamInTask, App.DAL.DTO.UserInTeamInTask>> _mapperMock;
    private readonly UserInTeamInTaskService _service;

    public UserInTeamInTaskServiceTest()
    {
        _repoMock = new Mock<IUserInTeamInTaskRepository>();
        _uowMock = new Mock<IAppUOW>();
        _mapperMock = new Mock<IBLLMapper<UserInTeamInTask, App.DAL.DTO.UserInTeamInTask>>();

        _uowMock.Setup(u => u.UserInTeamInTaskRepository).Returns(_repoMock.Object);

        _service = new UserInTeamInTaskService(_uowMock.Object, _mapperMock.Object);
    }


    [Fact]
    public void All_ReturnsMappedEntities()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var dalEntities = new List<App.DAL.DTO.UserInTeamInTask>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Since = DateTime.UtcNow.AddDays(-2),
                Until = DateTime.UtcNow.AddDays(2),
                TaskId = Guid.NewGuid(),
                UserInTeamId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Since = DateTime.UtcNow.AddDays(-1),
                Until = DateTime.UtcNow.AddDays(3),
                TaskId = Guid.NewGuid(),
                UserInTeamId = Guid.NewGuid()
            }
        };

        var bllEntities = new List<UserInTeamInTask>
        {
            new()
            {
                Id = dalEntities[0].Id,
                Since = dalEntities[0].Since,
                Until = dalEntities[0].Until,
                TaskId = dalEntities[0].TaskId,
                UserInTeamId = dalEntities[0].UserInTeamId
            },
            new()
            {
                Id = dalEntities[1].Id,
                Since = dalEntities[1].Since,
                Until = dalEntities[1].Until,
                TaskId = dalEntities[1].TaskId,
                UserInTeamId = dalEntities[1].UserInTeamId
            }
        };

        _repoMock.Setup(r => r.All(userId)).Returns(dalEntities);
        _mapperMock.Setup(m => m.Map(dalEntities[0])).Returns(bllEntities[0]);
        _mapperMock.Setup(m => m.Map(dalEntities[1])).Returns(bllEntities[1]);

        // Act
        var result = _service.All(userId);

        // Assert
        _repoMock.Verify(r => r.All(userId), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<App.DAL.DTO.UserInTeamInTask>()), Times.Exactly(2));

        Assert.NotNull(result);
        Assert.Collection(result, item => Assert.Equal(dalEntities[0].Id, item.Id),
            item => Assert.Equal(dalEntities[1].Id, item.Id));
    }

    [Fact]
    public async Task AllAsync_ReturnsMappedEntities()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var dalEntities = new List<App.DAL.DTO.UserInTeamInTask>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Since = DateTime.UtcNow.AddDays(-5),
                Until = DateTime.UtcNow.AddDays(5),
                TaskId = Guid.NewGuid(),
                UserInTeamId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Since = DateTime.UtcNow.AddDays(-3),
                Until = DateTime.UtcNow.AddDays(7),
                TaskId = Guid.NewGuid(),
                UserInTeamId = Guid.NewGuid()
            }
        };

        var bllEntities = new List<UserInTeamInTask>
        {
            new()
            {
                Id = dalEntities[0].Id,
                Since = dalEntities[0].Since,
                Until = dalEntities[0].Until,
                TaskId = dalEntities[0].TaskId,
                UserInTeamId = dalEntities[0].UserInTeamId
            },
            new()
            {
                Id = dalEntities[1].Id,
                Since = dalEntities[1].Since,
                Until = dalEntities[1].Until,
                TaskId = dalEntities[1].TaskId,
                UserInTeamId = dalEntities[1].UserInTeamId
            }
        };

        _repoMock.Setup(r => r.AllAsync(userId)).ReturnsAsync(dalEntities);
        _mapperMock.Setup(m => m.Map(dalEntities[0])).Returns(bllEntities[0]);
        _mapperMock.Setup(m => m.Map(dalEntities[1])).Returns(bllEntities[1]);

        // Act
        var result = await _service.AllAsync(userId);

        // Assert
        _repoMock.Verify(r => r.AllAsync(userId), Times.Once);
        _mapperMock.Verify(m => m.Map(It.IsAny<App.DAL.DTO.UserInTeamInTask>()), Times.Exactly(2));

        Assert.NotNull(result);
        Assert.Collection(result, item => Assert.Equal(dalEntities[0].Id, item.Id),
            item => Assert.Equal(dalEntities[1].Id, item.Id));
    }


    [Fact]
    public async Task FindAsync_ReturnsMappedEntity_WhenEntityExists()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = entityId,
            Since = DateTime.UtcNow,
            Until = DateTime.Now.AddDays(10),
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };
        var bllEntity = new UserInTeamInTask
        {
            Id = entityId,
            Since = dalEntity.Since,
            Until = dalEntity.Until,
            TaskId = dalEntity.TaskId,
            UserInTeamId = dalEntity.UserInTeamId
        };

        _repoMock.Setup(r => r.FindAsync(entityId, userId)).ReturnsAsync(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(bllEntity);

        // Act
        var result = await _service.FindAsync(entityId, userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(entityId, result.Id);
        Assert.Equal(bllEntity.Since, result.Since);

        _repoMock.Verify(r => r.FindAsync(entityId, userId), Times.Once);
        _mapperMock.Verify(m => m.Map(dalEntity), Times.Once);
    }

    [Fact]
    public async Task FindAsync_ReturnsNull_WhenEntityDoesNotExist()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _repoMock.Setup(r => r.FindAsync(entityId, userId)).ReturnsAsync((App.DAL.DTO.UserInTeamInTask?)null);

        // Act
        var result = await _service.FindAsync(entityId, userId);

        // Assert
        Assert.Null(result);
        _repoMock.Verify(r => r.FindAsync(entityId, userId), Times.Once);
    }


    [Fact]
    public void Add_CallsRepositoryAdd_WithMappedEntity()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var bllEntity = new UserInTeamInTask
        {
            Id = Guid.NewGuid(),
            Since = DateTime.UtcNow,
            Until = DateTime.Now.AddDays(10),
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };
        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = bllEntity.Id,
            Since = bllEntity.Since,
            Until = bllEntity.Until,
            TaskId = bllEntity.TaskId,
            UserInTeamId = bllEntity.UserInTeamId
        };

        _mapperMock.Setup(m => m.Map(bllEntity)).Returns(dalEntity);

        // Act
        _service.Add(bllEntity, userId);

        // Assert
        _mapperMock.Verify(m => m.Map(bllEntity), Times.Once);
        _repoMock.Verify(r => r.Add(dalEntity, userId), Times.Once);
    }


    [Fact]
    public async Task Add_ThenFind_ReturnsAddedEntity()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var bllEntity = new UserInTeamInTask
        {
            Id = entityId,
            Since = DateTime.UtcNow,
            Until = DateTime.Now.AddDays(10),
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };
        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = entityId,
            Since = bllEntity.Since,
            Until = bllEntity.Until,
            TaskId = bllEntity.TaskId,
            UserInTeamId = bllEntity.UserInTeamId
        };

        _mapperMock.Setup(m => m.Map(bllEntity)).Returns(dalEntity);
        _repoMock.Setup(r => r.Add(dalEntity, userId));


        // Act
        _service.Add(bllEntity, userId);


        // Assert
        _mapperMock.Verify(m => m.Map(bllEntity), Times.Once);
        _repoMock.Verify(r => r.Add(dalEntity, userId), Times.Once);


        // Arrange
        _repoMock.Setup(r => r.FindAsync(entityId, userId)).ReturnsAsync(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(bllEntity);


        // Act
        var foundEntity = await _service.FindAsync(entityId, userId);


        // Assert
        Assert.NotNull(foundEntity);
        Assert.Equal(entityId, foundEntity.Id);
        Assert.Equal(bllEntity.Since, foundEntity.Since);

        _repoMock.Verify(r => r.FindAsync(entityId, userId), Times.Once);
        _mapperMock.Verify(m => m.Map(dalEntity), Times.Once);
    }


    [Fact]
    public async Task Update_CallsRepositoryUpdate_WithMappedEntity()
    {
        // Arrange
        var bllEntity = new UserInTeamInTask
        {
            Id = Guid.NewGuid(),
            Since = DateTime.UtcNow,
            Until = null,
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };
        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = bllEntity.Id,
            Since = bllEntity.Since,
            Until = null,
            TaskId = bllEntity.TaskId,
            UserInTeamId = bllEntity.UserInTeamId
        };
        var userId = Guid.NewGuid();

        _mapperMock.Setup(m => m.Map(bllEntity)).Returns(dalEntity);
        _repoMock.Setup(r => r.UpdateAsync(dalEntity, userId)).ReturnsAsync(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(bllEntity);

        // Act
        var res = await _service.UpdateAsync(bllEntity, userId);

        // Assert
        Assert.NotNull(res);
        _mapperMock.Verify(m => m.Map(bllEntity), Times.Once);
        _repoMock.Verify(r => r.UpdateAsync(dalEntity, userId), Times.Once);
    }


    [Fact]
    public async Task Add_ThenUpdate_ReturnsUpdatedEntity()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var initialBllEntity = new UserInTeamInTask
        {
            Id = entityId,
            Since = DateTime.UtcNow,
            Until = DateTime.Now.AddDays(10),
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };
        var initialDalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = initialBllEntity.Id,
            Since = initialBllEntity.Since,
            Until = initialBllEntity.Until,
            TaskId = initialBllEntity.TaskId,
            UserInTeamId = initialBllEntity.UserInTeamId
        };

        _mapperMock.Setup(m => m.Map(initialBllEntity)).Returns(initialDalEntity);
        _repoMock.Setup(r => r.Add(initialDalEntity, userId));

        // Act
        _service.Add(initialBllEntity, userId);

        // Assert
        _mapperMock.Verify(m => m.Map(initialBllEntity), Times.Once);
        _repoMock.Verify(r => r.Add(initialDalEntity, userId), Times.Once);

        // Arrange for Update
        var newBllEntity = new UserInTeamInTask
        {
            Id = initialBllEntity.Id,
            Since = DateTime.UtcNow.AddDays(5),
            Until = null,
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };
        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = newBllEntity.Id,
            Since = newBllEntity.Since,
            Until = null,
            TaskId = newBllEntity.TaskId,
            UserInTeamId = newBllEntity.UserInTeamId
        };

        _mapperMock.Setup(m => m.Map(newBllEntity)).Returns(dalEntity);
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(newBllEntity);
        _repoMock.Setup(r => r.UpdateAsync(dalEntity, userId)).ReturnsAsync(dalEntity);

        // Act
        var res = await _service.UpdateAsync(newBllEntity, userId);

        // Assert
        Assert.NotNull(res);
        Assert.Equal(entityId, newBllEntity.Id);
        Assert.NotEqual(initialBllEntity.Since, newBllEntity.Since);
        Assert.NotEqual(initialBllEntity.Until, newBllEntity.Until);
        Assert.NotEqual(initialBllEntity.TaskId, newBllEntity.TaskId);
        Assert.NotEqual(initialBllEntity.UserInTeamId, newBllEntity.UserInTeamId);

        _mapperMock.Verify(m => m.Map(newBllEntity), Times.Once);
        _repoMock.Verify(r => r.UpdateAsync(dalEntity, userId), Times.Once);
    }


    [Fact]
    public void Remove_DoesNothing_WhenEntityNotFound()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _repoMock.Setup(r => r.Find(entityId, userId)).Returns((App.DAL.DTO.UserInTeamInTask?)null);

        // Act
        _service.Remove(entityId, userId);

        // Assert
        _repoMock.Verify(r => r.Find(entityId, userId), Times.Once);
        _repoMock.Verify(r => r.Remove(It.IsAny<App.DAL.DTO.UserInTeamInTask>(), userId), Times.Never);
    }


    [Fact]
    public async Task RemoveAsync_RemovesEntity_WhenFound()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = entityId,
            Since = DateTime.UtcNow.AddDays(-1),
            Until = DateTime.UtcNow.AddDays(1),
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };

        _repoMock.Setup(r => r.FindAsync(entityId, userId)).ReturnsAsync(dalEntity);
        _repoMock.Setup(r => r.RemoveAsync(entityId, userId)).Returns(Task.CompletedTask);

        // Act
        await _service.RemoveAsync(entityId, userId);

        // Assert
        _repoMock.Verify(r => r.FindAsync(entityId, userId), Times.Once);
        _repoMock.Verify(r => r.RemoveAsync(entityId, userId), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_DoesNothing_WhenEntityNotFound()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _repoMock.Setup(r => r.FindAsync(entityId, userId)).ReturnsAsync((App.DAL.DTO.UserInTeamInTask?)null);

        // Act
        await _service.RemoveAsync(entityId, userId);

        // Assert
        _repoMock.Verify(r => r.FindAsync(entityId, userId), Times.Once);
        _repoMock.Verify(r => r.RemoveAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public void Exists_ReturnsTrue_WhenEntityExists()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = entityId,
            Since = DateTime.UtcNow,
            Until = DateTime.UtcNow.AddDays(2),
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };

        _repoMock.Setup(r => r.Find(entityId, userId)).Returns(dalEntity);

        // Act
        var result = _service.Exists(entityId, userId);

        // Assert
        _repoMock.Verify(r => r.Find(entityId, userId), Times.Once);
        Assert.True(result);
    }

    [Fact]
    public async Task ExistsAsync_ReturnsFalse_WhenEntityDoesNotExist()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        _repoMock.Setup(r => r.FindAsync(entityId, userId)).ReturnsAsync((App.DAL.DTO.UserInTeamInTask?)null);

        // Act
        var result = await _service.ExistsAsync(entityId, userId);

        // Assert
        _repoMock.Verify(r => r.FindAsync(entityId, userId), Times.Once);
        Assert.False(result);
    }


    [Fact]
    public async Task Add_Find_ThenDelete_SuccessfulWorkflow()
    {
        // Arrange
        var entityId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var bllEntity = new UserInTeamInTask
        {
            Id = entityId,
            Since = DateTime.UtcNow,
            Until = DateTime.UtcNow,
            TaskId = Guid.NewGuid(),
            UserInTeamId = Guid.NewGuid()
        };
        var dalEntity = new App.DAL.DTO.UserInTeamInTask
        {
            Id = entityId,
            Since = bllEntity.Since,
            Until = bllEntity.Until,
            TaskId = bllEntity.TaskId,
            UserInTeamId = bllEntity.UserInTeamId
        };

        _mapperMock.Setup(m => m.Map(bllEntity)).Returns(dalEntity);
        _repoMock.Setup(r => r.Add(dalEntity, userId));

        // Act
        _service.Add(bllEntity, userId);

        // Assert
        _mapperMock.Verify(m => m.Map(bllEntity), Times.Once);
        _repoMock.Verify(r => r.Add(dalEntity, userId), Times.Once);
        bool isRemoved = false;


        // Arrange
        _repoMock.Setup(r => r.FindAsync(entityId, userId)).ReturnsAsync(() =>
            isRemoved ? null : dalEntity
        );
        _mapperMock.Setup(m => m.Map(dalEntity)).Returns(bllEntity);

        // Act
        var foundEntity = await _service.FindAsync(entityId, userId);

        // Assert
        Assert.NotNull(foundEntity);
        Assert.Equal(entityId, foundEntity.Id);
        Assert.Equal(bllEntity.Since, foundEntity.Since);
        Assert.Equal(bllEntity.Until, foundEntity.Until);
        Assert.Equal(bllEntity.TaskId, foundEntity.TaskId);
        Assert.Equal(bllEntity.UserInTeamId, foundEntity.UserInTeamId);

        _repoMock.Verify(r => r.FindAsync(entityId, userId), Times.Once);
        _mapperMock.Verify(m => m.Map(dalEntity), Times.Once);


        // Arrange
        _repoMock.Setup(r => r.Find(entityId, userId)).Returns(() => isRemoved ? null : dalEntity);

        _repoMock.Setup(r => r.Remove(It.IsAny<App.DAL.DTO.UserInTeamInTask>(), userId))
            .Callback(() => isRemoved = true);

        // Act
        _service.Remove(entityId, userId);

        // Assert
        _repoMock.Verify(r => r.Remove(It.IsAny<App.DAL.DTO.UserInTeamInTask>(), userId), Times.Once);


        // Act
        var foundEntityAfterRemoval = await _service.FindAsync(entityId, userId);

        // Assert
        Assert.Null(foundEntityAfterRemoval);
    }
}