const pipeDatetime = (date) => {
    const dateArr = date.split('T');
    return `${dateArr[0]}`;
}

export { pipeDatetime }