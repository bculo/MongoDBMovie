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

export interface ICharacterRequest{
    movieId: string,
}

export interface ICharacter{
    id: string,
    characterInMovie: string,
    name: string,
    profilePath: string
}

export interface IGenreRequest{
    movieId: string,
}

export interface IGenre{
    id: string,
    name: string
}