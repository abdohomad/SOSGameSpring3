
namespace SOSGameLogic.Interfaces
{
    public interface IPlayer
    {
        char GetPlayerSymbol();
        void IncreaseScore(int points);
        int GetScore();
        void SetPlayerSymbol(char symbol);
        void ResetScore();
    }
}
