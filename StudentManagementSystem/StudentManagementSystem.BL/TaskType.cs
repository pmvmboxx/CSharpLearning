using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.BL
{
    [Flags]
    public enum TaskType
    {
        Exit             = 0b00000000,
        AddNewStudent    = 0b00000001,
        CreateNewGroup   = 0b00000010,
        FindAStudent     = 0b00000100,
        FindAGroup       = 0b00001000,
        EditAStudent     = 0b00010000,
        TransferAStudent = 0b00100000,
        RemoveAStudent   = 0b01000000
    }
}
