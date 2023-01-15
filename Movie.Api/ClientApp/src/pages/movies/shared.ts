export const movieYear = (releaseDate: Date) => {
    if (!releaseDate) return releaseDate;
    return new Date(releaseDate).getFullYear()
}
