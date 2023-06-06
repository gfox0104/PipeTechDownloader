namespace PipeTech.Downloader.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
