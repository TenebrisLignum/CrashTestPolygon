const ACCESS_TONKEN_KEY = 'token';

export default class LocalStorageHelper {
    static set(key: string, value: string) {
        localStorage.setItem(key, value);
    }

    static get(key: string): string | null {
        return localStorage.getItem(key);
    }

    static getAccessToken(): string | null {
        return this.get(ACCESS_TONKEN_KEY);
    }

    // TODO: Move refresh token from the local storage
    static setAccessToken(token: string) {
        this.set(ACCESS_TONKEN_KEY, token);
    }
}