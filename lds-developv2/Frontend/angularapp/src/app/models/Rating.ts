export class Rating {

  constructor(
    public ratingID?: number, // Id da avaliação
    public userID?: number, // ID do utilizador que efetuou a avaliação
    public recintoID?: number, // Id do recinto que foi avaliado
    public username?: string, // nome do utilizador que efetuou a avaliação
    public rating?: number, // rate {1-5}
    public description?: string, // Descrição caso o utilizador queira
  ) { } 
}
