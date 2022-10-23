import { initializeApp } from 'firebase/app'
import { getStorage } from "firebase/storage";

const firebaseConfig = {
    apiKey: "AIzaSyAOOcOUf9WmAAqu7uEiqqpAVZ92nfRJgZ8",
    authDomain: "testproject-1ec77.firebaseapp.com",
    projectId: "testproject-1ec77",
    storageBucket: "testproject-1ec77.appspot.com",
    messagingSenderId: "246356743472",
    appId: "1:246356743472:web:6016ec6e8dcc1ddc6064f5"
};

const firebaseApp = initializeApp(firebaseConfig);
// initializeApp(firebaseConfig);

const storage = getStorage(firebaseApp);

export {
    storage,
}