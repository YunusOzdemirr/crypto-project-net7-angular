import { Address } from '../AddressModel/addressModel';

export default interface User {
  name: string;
  email: string;
  address: Address;
}
