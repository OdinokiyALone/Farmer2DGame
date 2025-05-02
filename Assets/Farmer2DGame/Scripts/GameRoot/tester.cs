using UnityEngine;
using UnityEngine.InputSystem;

public class tester : MonoBehaviour, Pusher.IPlayerActions
{
    
    public bool ShowUI = false;
    
    private Pusher input;

    private void Awake()
    {
        input = new Pusher();
        input.Player.SetCallbacks(this);

    }
    private void Update()
    {
        if(MainManager.Manager == null) { return; }
        if (ShowUI)
        {
            MainManager.Manager.UIManager.ShowUI("MainMenu");
        }
        else
        {
            MainManager.Manager.UIManager.HideUI("MainMenu");
        }
    }


    

     
    #region Input
    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        
    }

    #endregion
    
   
}