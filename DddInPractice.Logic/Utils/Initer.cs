using DddInPractice.Logic.Common;
using DddInPractice.Logic.Management;
using DddInPractice.Logic.Utils;

namespace DddInPractice.Logic;

public static class Initer
{
    public static void Init(string connectionString)
    {
        SessionFactory.Init(connectionString);
        HeadOfficeInstance.Init();
        DomainEvents.Init();
    }
}