using System.Threading.Tasks;
using dotnet_core_reaction_speed_game.enums;

namespace dotnet_core_reaction_speed_game
{
    public interface IInformationService
    {
        Task Init();

        void SendMsg(string topic, string message);

        bool GetConnectionStatus();

    }
}