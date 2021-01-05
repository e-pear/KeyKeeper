using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyKeeper.Tabs
{
    /// <summary>
    /// Interface for UI presented objects. Object implementing the interface mirrors in partial or complex way objects from model layer. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPresentedRecordOf<T>
    {
        // Every model object should have some Id property, and should be able to provide it (in my case in two types).
        string GetPresentedIdCode();
        int? GetPresentedIdNumber();
        
        // Inidictes whether values of presented record (input by user) are equal to those selected from model.
        bool IsEqualToSelectedRecord(T selectedRecord);
        // Indicates whether presented object has filled and validated all required fields, thus is able to be saved in model.
        bool IsSaveable();
        // Ability to create a proper model object from presented one.
        T GetRecordFromPresentedRecord();

        // Ability to update presented record according to currently selected "model" object.
        void SynchronizePresentedRecordWith(T selectedRecord);
        // Clear values from presented model.
        void ClearPresentedRecord();
        // Allows to set/overwrite id number of presented record object.
        void SetPresentedIdCode(string code);

    }
}
