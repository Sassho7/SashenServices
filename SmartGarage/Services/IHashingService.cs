namespace SmartGarage.Services;

public interface IHashingService
{
    string Sha256(string input);
}