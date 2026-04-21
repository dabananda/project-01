export interface Person {
  personId: number;
  personName: string;
  personDoB: string;
  personHeight: number;
  personWeight: number;
  personGender: string;
  personMaritalStatus: string;
  personIsGraduated: boolean;
}

export interface PersonCreateUpdate {
  personName: string;
  personDoB: string;
  personHeight: number;
  personWeight: number;
  personGender: string;
  personMaritalStatus: string;
  personIsGraduated: boolean;
}
