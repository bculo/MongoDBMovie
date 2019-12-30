export interface IMovie{
    id: string,
    posterPath: string,
    title: string,
    imdbId: number,
    imdbRating: string,
    overview: string,
    releasedate: string
}

export interface ITitleMovieRequest{
    title: string,
    page: number
}