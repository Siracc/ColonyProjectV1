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
    [SerializeField] Toggle _rememberToggle;
    [SerializeField] Button _registerOrLogin;
    [SerializeField] Text _registerOrLoginText, _alreadyText;
    [Header("Guest Login Settings")]
    [SerializeField] bool _GuestLogin;
    [SerializeField] GameObject _emailRegisterGO, _passwordRegisterGO, _usernameRegisterGO, _repeatPasswordRegisterGO, _passwordLoginGO, _usernameAndEmailLoginGO, _rememberToggleGO;

   
    #region RegisterLogin System
    void LoginEmail()
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest() {Email = _usernameAndEmailLogin.text, Password = _passwordLogin.text}, Result => { Debug.Log("Giriş Başarılı"); }, Error => { Debug.Log("Giriş Başarışız!"); });
    }

    void LoginUsername()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest() { Username = _usernameAndEmailLogin.text, Password = _passwordLogin.text }, Result => { Debug.Log("Giriş Başarılı"); }, Error => { Debug.Log("Giriş Başarışız!"); });
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
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest() { Username = _usernameRegister.text, Email = _emailReğister.text ,Password = _passwordRegister.text }, Result => { Debug.Log("Giriş Başarılı"); }, Error => { Debug.Log("Giriş Başarışız!"); });
    }

    public void RememberMe()
    {
        if (_rememberToggle.isOn)
        {
            PlayerPrefs.SetString("emailOrUsername", _usernameAndEmailLogin.text);
            PlayerPrefs.SetString("password", _passwordLogin.text);
        }

    }

    public void PlayGuest()
    {
        PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest() { CreateAccount = _GuestLogin }, Result => { }, Error => { });
    }
    public void SwitchLoginOrRegister()
    {
        switch(_emailRegisterGO.activeInHierarchy)
        {
            case false:
                _usernameAndEmailLoginGO.SetActive(false);
                _passwordLoginGO.SetActive(false);
                _emailRegisterGO.SetActive(true);
                _passwordRegisterGO.SetActive(true);
                _repeatPasswordRegisterGO.SetActive(true);
                _usernameRegisterGO.SetActive(true);
                _registerOrLoginText.text = "Register";
                _alreadyText.text = "Login";
                _rememberToggleGO.SetActive(false);
                _registerOrLogin.onClick.RemoveListener(SwitchLoginType);
                _registerOrLogin.onClick.AddListener(Register);
                break;
            default:
                _usernameAndEmailLoginGO.SetActive(true);
                _passwordLoginGO.SetActive(true);
                _emailRegisterGO.SetActive(false);
                _passwordRegisterGO.SetActive(false);
                _repeatPasswordRegisterGO.SetActive(false);
                _usernameRegisterGO.SetActive(false);
                _registerOrLoginText.text = " Login";
                _alreadyText.text = "Register";
                _registerOrLogin.onClick.RemoveListener(Register);
                _registerOrLogin.onClick.AddListener(SwitchLoginType);

                break;
        }
    }
    #endregion
}
