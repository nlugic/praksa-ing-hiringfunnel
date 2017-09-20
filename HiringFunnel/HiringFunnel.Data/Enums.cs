
namespace HiringFunnel.Data
{

    public enum SeniorityLevel : byte
    {
        Entry = 0,
        Junior = 1,
        Medior = 2,
        Senior = 5
    }

    public enum UserLevel : byte
    {
        Admin,
        HR,
        Interviewer
    }

    public enum Phase : byte
    {
        Contact_Comment,
        Contact_Phase,
        Interview_Phase,
        Test_Phase,
        TestDefense_Phase,
        Offer_Phase,
        Acceptance_Phase,
        Rejection_Phase,
        Quit_Phase
    }

}
