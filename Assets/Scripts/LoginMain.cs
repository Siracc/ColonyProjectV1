using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class LoginMain : MonoBehaviour
{

 
    [SerializeField] InputField _emailReğister, _passwordRegister, _usernameRegister, _repeatPasswordRegister;
    [SerializeField] InputField _passwordLogin, _usernameAndEmailLogin;
    [SerializeField] Button _registerButton, _loginButton;
    //[SerializeField] Text _resultText;
    [Header("Guest Login Settings")]
    [SerializeField] bool _guestLogin;
    [SerializeField] GameObject _registerPanel, _loginPanel;
    [SerializeField] Animator _animator;


    #region RegisterLogin System

    private void Start()
    {
        SwitchLoginOrRegister();
    }

    void LoginEmail()
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest() 
        {
            Email = _usernameAndEmailLogin.text, 
            Password = _passwordLogin.text
        }, 
        Result => 
        { 
            Debug.Log("Giriş Başarılı");
        }, 
        Error => 
        { 
            Debug.Log("Giriş Başarışız!"); 
        });
    }

    void LoginUsername()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest() 
        { 
            Username = _usernameAndEmailLogin.text, 
            Password = _passwordLogin.text 
        }, 
        Result => 
        { 
            Debug.Log("Giriş Başarılı"); 
        }, 
        Error => 
        { 
            Debug.Log("Giriş Başarışız!"); 
        });
    }

    public void SwitchLoginType() 
    {
        if (_usernameAndEmailLogin.text.IndexOf('@') < 0)
        {
            LoginEmail();
        }
        else
            LoginUsername();
    }

    public void Register()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest() 
        { 
            Username = _usernameRegister.text, 
            Email = _emailReğister.text,
            Password = _passwordRegister.text 
        }, 
        Result => 
        {
            _animator.Play("Succes");
            Debug.Log("Kayıt Başarılı"); 
        }, 
        Error => 
        { 
            Debug.Log("Kayıt Başarışız!"); 
        });
    }

    public void RememberMe()
    {  
            PlayerPrefs.SetString("emailOrUsername", _usernameAndEmailLogin.text);
            PlayerPrefs.SetString("password", _passwordLogin.text);
    }

    public void PlayGuest()
    {
        PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest() { CreateAccount = _guestLogin, AndroidDeviceId = SystemInfo.deviceUniqueIdentifier}, Result => { Debug.Log("Misafir Girişi başarılı"); }, Error => { Debug.Log("misafir girişi başarısız"); });
    }
    public void SwitchLoginOrRegister()
    {
        switch (_registerPanel.activeInHierarchy)
        {
            case true:
                _loginPanel.SetActive(true);
                _registerPanel.SetActive(false);
                break;
            default:
                _loginPanel.SetActive(false);
                _registerPanel.SetActive(true);
                break;
        }
    }
    #endregion
}
