export interface Person {
  personId: number;
  personName: string;
  personDateOfBirth: string;
  personHeightInFeet: number;
  personWeightInKg: number;
  personGender: string;
  personMaritalStatus: string;
  personIsGraduated: boolean;
}

export interface PersonCreateUpdate {
  personName: string;
  personDateOfBirth: string;
  personHeightInFeet: number;
  personWeightInKg: number;
  personGender: string;
  personMaritalStatus: string;
  personIsGraduated: boolean;
}
