export default class LocalStorageHelper {

    static add(key: string, value: string) {
        localStorage.setItem(key, value);
    }

    static get(key: string): string {
        return localStorage.getItem("key") ?? '';
    }
}