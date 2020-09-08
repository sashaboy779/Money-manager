using AutoMapper;
using Moq;
using DataAccessLayer.UnitOfWork;

namespace BusinessLogicLayerTest.Tests
{
    public abstract class ServiceTest
    {
      protected readonly Mock<IUnitOfWork> MockUnitOfWork;
      protected readonly Mock<IMapper> MockMapper;

      protected ServiceTest()
      {
        MockUnitOfWork = new Mock<IUnitOfWork>();
        MockMapper = new Mock<IMapper>();
      }
    }
}