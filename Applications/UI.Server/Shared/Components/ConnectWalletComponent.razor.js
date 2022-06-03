console.log("Loaded ConnectWalletComponent.razor.js");

window.wallet = async () => {
    if (typeof window.ethereum !== 'undefined') {
        console.log('Wallet is detected!');
        try {
            const accounts = await ethereum.request({ method: 'eth_requestAccounts' });
            const account = accounts[0];
            console.log("Account found: " + account);
            return account;
        } catch (error) {
            console.error(error);
            return null;
        }
    }
    else {
        console.log("Wallet not detected!");
    }
}