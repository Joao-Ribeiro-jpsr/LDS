export class Users {
  constructor(
    public userID?: number, // ID do utilizador
    public userContactsID?: number, // ID do contacto do utilizador
    public nome?: string, // Nome do utilizador
    public email?: string, // Email do utilizador
    public password?: string, // Password do utilizador
    public isAdmin?: boolean, // Role do utilizador
    public morada?: string, // Morada do utilizador
    public genero?: string, // Genero do utilizador
    public nascimento?: string, // Data de nascimento do utilizador
    public telemovel?: string, // Telemovel do utilizador
    public nif?: string, // Nif do utilizador
    public isAccountActivated?: boolean, // Atributo para verificar se a conta está bloqueada ou não do utilizador
    public pontos?: number // Pontos do utilizador
  ) { }
}
