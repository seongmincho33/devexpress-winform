using System;
using WinFormMVC.Model;

namespace WinFormMVC.Controller
{
    public interface IUsersView
    {
        void SetController(UsersController controller);
        void ClearGrid();
        void AddUserToGrid(User user);
        void UpdateGridWithChangedUser(User user);
        void RemoveUserFromGrid(User user);
        string GetIdOfSelectedUserInGrid();
        void SetSelectedUserInGrid(User user);
        
        string FirstName     { get; set; }
        string LastName      { get; set; }
        string ID            { get; set; }
        string Department    { get; set; }
        User.SexOfPerson Sex { get; set; }  
        bool CanModifyID     {      set; }
    }
}
