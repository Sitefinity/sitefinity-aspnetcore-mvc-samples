using System;

namespace migrate_data_to_cms.Samples.Choices
{
    public enum SingleChoice
    {
        FirstChoice,
        SecondChoice,
        ThirdChoice
    }

    [Flags]
    public enum MultipleChoice
    {
        FirstChoice = 0x01,
        SecondChoice = 0x02,
        ThirdChoice = 0x04
    }
}