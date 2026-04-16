export interface Person {
    id: number;
    name: string;
    dateOfBirth: string;
    heightInFeet: number;
    weightInKg: number;
    gender: string;
    maritalStatus: string;
    isGraduated: boolean;
}

export interface PersonCreateUpdate {
    name: string;
    dateOfBirth: string;
    heightInFeet: number;
    weightInKg: number;
    gender: string;
    maritalStatus: string;
    isGraduated: boolean;
}