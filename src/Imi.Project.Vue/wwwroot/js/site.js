"use strict"
const apiLink = "https://localhost:7001/api/";
const pokeAmount = 898;

function getCookieValue(cookieKey) {
    if (typeof (cookieKey) !== 'string') return;

    let cookies = document.cookie.split(';');
    if (cookies.length <= 0) return;

    for (let i = 0; i < cookies.length; i++) {
        let splitCookie = cookies[i].split('=');
        let key = splitCookie[0].trim();
        let value = splitCookie[1].trim();
        if (key === cookieKey) return value;
    }
}

function writeCookie(cookieKey, cookieValue) {
    if (typeof (cookieKey) !== 'string' || typeof (cookieValue) !== 'string') return;
    document.cookie = `${cookieKey}=${cookieValue};`;
}

function createFormData(formItem) {
    let form = new FormData();
    for (let item in formItem) {
        if (item !== "imageUrl") {
            form.append(item, formItem[item]);
        }
    }
    return form;
}

function toPageAmount(itemCount) {
    if (isNaN(itemCount)) return;

    return Math.ceil(itemCount / 10);
}

function toCapitalFirstLetter(word) {
    return word[0].toUpperCase() + word.slice(1);
}

function handleError(responseData) {
    if (responseData.errors === undefined) {
        return responseData;
    }

    let result = "";

    for (let item in responseData.errors) {
        result += `${responseData.errors[item]}\n`;
    }
    return result;
}