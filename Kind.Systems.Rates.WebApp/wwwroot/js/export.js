
// демонстрация JS просто, можно на C# сделать 
export async function fetchAndSave(apiUrl, fileName) {
    const resp = await fetch(apiUrl, { cache: 'no-store', credentials: 'include' });
    if (!resp.ok) throw new Error(`HTTP ${resp.status}`);

    // Создаем ссылку для скачивания файла
    const blob = await resp.blob();
    const link = Object.assign(document.createElement('a'), {
        href: URL.createObjectURL(blob),
        download: fileName,
        style: 'display:none'
    });

    document.body.append(link);
    link.click();
    link.remove();
    URL.revokeObjectURL(link.href);
}
