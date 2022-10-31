using NHibernate.Event;

namespace DddInPractice.Logic.Management;

public static class HeadOfficeInstance
{
    private const long HeadOfficeId = 1;
    public static HeadOffice Instance { get; private set; }

    public static void Init()
    {
        var headOfficeRepository = new HeadOfficeRepository();
        Instance = headOfficeRepository.GetById(HeadOfficeId);
    }
}