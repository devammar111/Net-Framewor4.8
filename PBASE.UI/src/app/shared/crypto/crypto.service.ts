import * as CryptoJS from 'crypto-js';

export class CryptoService {
    encryptedBase: string;

    //Encrypt Message
    encrypt(data: number) {
        
        let key = "HackersSeeIT2";
        let iv = CryptoJS.enc.Base64.parse("#base64IV#");
        let encrypted = CryptoJS.AES.encrypt(String(data), key, { iv: iv });
        this.encryptedBase = String(encrypted);
        return btoa(this.encryptedBase);
    }

    //Decrypt Message
    decrypt(data: string) {
        if (data != undefined || data != null) {
            data = atob(data);
            let password = "HackersSeeIT2";
            let value = data.replace(" ", "+");
            let decrypted = CryptoJS.AES.decrypt(String(value), password);
            let id = decrypted.toString(CryptoJS.enc.Utf8);
            return +id;
        }
    }

    encryptString(data: string) {

        let key = "HackersSeeIT2";
        let iv = CryptoJS.enc.Base64.parse("#base64IV#");
        let encrypted = CryptoJS.AES.encrypt(String(data), key, { iv: iv });
        this.encryptedBase = String(encrypted);
        return btoa(this.encryptedBase);
    }

    //Decrypt String Message
    decryptString(data: string) {
        if (data != undefined || data != null) {
            data = atob(data);
            let password = "HackersSeeIT2";
            let value = data.replace(" ", "+");
            let decrypted = CryptoJS.AES.decrypt(String(value), password);
            let val = decrypted.toString(CryptoJS.enc.Utf8);
            return val;
        }
    }
}