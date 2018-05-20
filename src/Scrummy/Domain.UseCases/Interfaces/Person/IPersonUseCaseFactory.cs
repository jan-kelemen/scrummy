namespace Scrummy.Domain.UseCases.Interfaces.Person
{
    public interface IPersonUseCaseFactory
    {
        ICreatePersonUseCase Create { get; }

        IViewPersonUseCase View { get; }

        IEditPersonUseCase Edit { get; }

        IChangePasswordUseCase ChangePassword { get; }

        IViewCurrentWorkUseCase ViewCurrentWork { get; }
    }
}
