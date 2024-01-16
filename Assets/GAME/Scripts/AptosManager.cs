using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aptos.HdWallet;
using NBitcoin;
using Aptos.Unity.Rest;
using Aptos.Unity.Rest.Model;


/// <summary>
/// The main entry point into the Aptos blockchain.
/// </summary>

public class AptosManager : MonoBehaviour
{
  private string _mnemonicsKey = "AptosMnemonicsKey";
  private Wallet _wallet;
  private string faucetEndpoint = "https://faucet.devnet.aptoslabs.com";

  void Start()
  {
    RestClient.Instance.SetEndPoint(Constants.DEVNET_BASE_URL);
  }

  [ContextMenu("InitWalletFromCache")]
  public void InitWalletFromCache()
  {
    string mnemonics = PlayerPrefs.GetString(_mnemonicsKey);
    _wallet = new Wallet(mnemonics);
    GetCurrentWalletAddress();
    //LoadCurrentWalletBalance();
  }

  [ContextMenu("GetCurrentWalletAddress")]
  public string GetCurrentWalletAddress()
  {
    var account = _wallet.GetAccount(0);
    var addr = account.AccountAddress.ToString();
    Debug.Log("GetCurrentWalletAddress: " + addr);
    return addr;
  }

  [ContextMenu("CreateNewWallet")]
  public bool CreateNewWallet()
  {
    Mnemonic mnemo = new Mnemonic(Wordlist.English, WordCount.Twelve);
    _wallet = new Wallet(mnemo);

    PlayerPrefs.SetString(_mnemonicsKey, mnemo.ToString());

    GetCurrentWalletAddress();
    Debug.Log("CreateNewWallet: " + mnemo.ToString());
    //LoadCurrentWalletBalance();
    if (mnemo.ToString() != string.Empty)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  [ContextMenu("LoadCurrentWalletBalance")]
  public void LoadCurrentWalletBalance()
  {
    AccountResourceCoin.Coin coin = new AccountResourceCoin.Coin();
    ResponseInfo responseInfo = new ResponseInfo();
    var accountAddress = _wallet.GetAccount(0).AccountAddress;

    StartCoroutine(RestClient.Instance.GetAccountBalance((_coin, _responseInfo) =>
    {
      coin = _coin;
      responseInfo = _responseInfo;

      if (responseInfo.status != ResponseInfo.Status.Success)
      {
        Debug.Log("LoadCurrentWalletBalance: " + responseInfo.message);
        //onGetBalance?.Invoke(0.0f);
      }
      else
      {
        float balance = float.Parse(coin.Value);
        Debug.Log("LoadCurrentWalletBalance: " + balance);
        //onGetBalance?.Invoke(balance);
      }

    }, accountAddress));
  }

  public IEnumerator SendToken(string _targetAddress, long _amount)
  {

    var transferTxn = new Aptos.Unity.Rest.Model.Transaction();
    ResponseInfo responseInfo = new ResponseInfo();
    Coroutine transferCor = StartCoroutine(RestClient.Instance.Transfer((_transferTxn, _responseInfo) =>
    {
      transferTxn = _transferTxn;
      responseInfo = _responseInfo;
    }, _wallet.GetAccount(0), _targetAddress, _amount));

    yield return transferCor;

  }

  [ContextMenu("AirDrop")]
  public void AirDrop()
  {
    int _amount = 1000000000;
    Coroutine cor = StartCoroutine(FaucetClient.Instance.FundAccount((success, returnResult) =>
    {
    }, _wallet.GetAccount(0).AccountAddress.ToString()
        , _amount
        , faucetEndpoint));

  }


}
